FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TicketType.Microservice.Template.csproj", "./"]
RUN dotnet restore "TicketType.Microservice.Template.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TicketType.Microservice.Template.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketType.Microservice.Template.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketType.Microservice.Template.dll"]
