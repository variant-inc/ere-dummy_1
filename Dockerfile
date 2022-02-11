# https://medium.com/01001101/containerize-your-net-core-app-the-right-way-35c267224a8d
ARG VERSION=5.0-alpine
FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build-env
# GITHUB ARGS intentionally after FROM so they can be used in build-env
ARG GITHUB_USER
ARG GITHUB_TOKEN
ENV DOTNET_RUNNING_IN_CONTAINER=true
WORKDIR /app
RUN apk add --no-cache tzdata

COPY ./*.sln .
COPY ./nuget.config .
COPY ./Directory.Build.props .
COPY ./src/TicketType.Microservice.Template/*.csproj src/TicketType.Microservice.Template/
COPY ./tests/TicketType.Microservice.Template.UnitTests/*.csproj tests/TicketType.Microservice.Template.UnitTests/
RUN dotnet restore

COPY ./.config .
RUN dotnet tool restore

COPY ./src src
COPY ./tests tests
RUN dotnet build

FROM build-env AS test-env
RUN ["dotnet", "test"]
CMD ["dotnet", "test"]

FROM build-env AS publish
RUN dotnet publish src/TicketType.Microservice.Template -c Release -o ./output

FROM mcr.microsoft.com/dotnet/aspnet:$VERSION
RUN apk add --no-cache tzdata
ENV DOTNET_RUNNING_IN_CONTAINER=true
WORKDIR /app
COPY --from=publish /app/output .

RUN adduser -S --home /app app && chown -R app /app

# we're all done with APK, good bye
RUN rm -f /sbin/apk && \
    rm -rf /etc/apk && \
    rm -rf /lib/apk && \
    rm -rf /usr/share/apk && \
    rm -rf /var/lib/apk
    
USER app

ENTRYPOINT ["dotnet", "TicketType.Microservice.Template.dll"]