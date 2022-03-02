# Ticket Type Microservice GitHub Template
This is a GitHub Template repository that subsequent microservices can be cloned from.
It will serve as the basis for concrete implementations of the ERE Ticket Type Microservices.

# Function
This template serves to provide the following:
- General deployment set up & configuration.
- Importing of shared libraries.
- Basic shared set up for determining service type based on incoming API data.

# Usage
- Navigate to https://github.com/variant-inc/ticket-type-microservice-template/generate
- Complete the required steps. * BE SURE to create your repository with the 'variant-inc' owner.
  - Example repository name: `variant-inc/some-microservice`

# Setup
Once you have created your new repository you need to do some basic setup.
  - Open`.github/workflows/octo-push.yaml`
    - Update the `HELM_CHART_PATH` environment variable to match the new project.
  - Rename `src/TicketType.Microservice.Template` to the new name.
  - Rename `src/TicketType.Microservice.Template`
  - Update all instances of `TicketType.Microservice.Template` namespace throughout the code base to the new namespace.
  - Setup Octopus:
    - Clone the existing [Microservice Template Project](https://octopus.apps.ops-drivevariant.com/app#/Spaces-22/projects/ticket-type-microservice-template) project at [https://octopus.com/docs/projects#clone-a-project](https://octopus.apps.ops-drivevariant.com/app#/Spaces-22/projects/ticket-type-microservice-template/deployments)
  - Setup SonarCloud:
    - See: https://drivevariant.atlassian.net/wiki/spaces/CLOUD/pages/2023555115/Create+Project+for+SonarScan
    - navigate to https://sonarcloud.io/projects/create
    - Search for the new GitHub repo and import it.
    - Be sure to address the automatic analysis issues mentioned in the confluence page above.
  - 