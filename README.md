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
- Once you have created your new repository you need to:
  - Copy `.github/workflows/octo-push.yaml.example` -> `.github/workflows/octo-push.yaml`
  - Find & replace '<REPLACE-THIS>' with the appropriate values.
  - Correct the namespaces in: `src/TicketType.Microservice.Template/Infrastructure/FeatureFlags/*`