# Ticket Type Microservice GitHub Template
This is a GitHub Template repository that subsequent microservices can be cloned from.
It will serve as the basis for concrete implementations of the ERE Ticket Type Microservices.

# Function
This template serves to provide the following:
- General deployment set up & configuration.
- Importing of shared libraries.
- Basic shared set up to provide all necessary functionality for a microservice to:
  - Terraform the appropriate incoming SQS queue, subscribe that queue to the Solutions Orchestrator SNS topic, & an outgoing SNS topic.
  - receive SQS messages.
  - determine entity & event types.
  - determine job start & job end events.
  - do processing using some business logic interface.
  - publishing messages to an SNS topic using some batch functionality.

# Usage
- Navigate to https://github.com/variant-inc/ticket-type-microservice-template/generate
- Complete the required steps. ***BE SURE** to create your repository with the 'variant-inc' owner.
  - Example repository name: `variant-inc/some-microservice`
- Be sure to update the permissions of the new repo to include:
  - Add "Engineering Leadership" with role "Admin"
  - Add "Engineering" with role "Write"

# Setup
Once you have created your new repository you need to do some basic setup.

### Octopus:
- Clone the existing [Microservice Template Project](https://octopus.apps.ops-drivevariant.com/app#/Spaces-22/projects/ticket-type-microservice-template) project at [https://octopus.com/docs/projects#clone-a-project](https://octopus.apps.ops-drivevariant.com/app#/Spaces-22/projects/ticket-type-microservice-template/deployments)
- Update the `ECR_REPO_URL` value (this will need Admin privs to set it for all environments):
  - Format: 064859874041.dkr.ecr.us-east-1.amazonaws.com/exception-recognition-engine/<NAME>
  - The domain will always be '064859874041.dkr.ecr.us-east-1.amazonaws.com'.
  - The namespace will always be 'exception-recognition-engine'.
  - Update the `<NAME>` part to match the project name.
  - Example:
    - Project name = 'somename'
    - URL: 064859874041.dkr.ecr.us-east-1.amazonaws.com/exception-recognition-engine/somename
### SonarCloud:
- See: https://drivevariant.atlassian.net/wiki/spaces/CLOUD/pages/2023555115/Create+Project+for+SonarScan
- navigate to https://sonarcloud.io/projects/create
- Search for the new GitHub repo and import it.
### Local Stuff
- Run the following script. It will:
  - (This is a required step) remove some unnecessary Terraform files.
  - remove this README file & replace it with a new one for you to customize for this service.
  - `./scripts/project-setup.sh`
  - If for some reason you can't run this, simply look at the script and do the steps manually.
  - Look for 'CHANGE_THIS' & replace with the appropriate value.

# Running Locally
- Open a terminal & navigate to the repo root.
- Run `docker-compose up --build`
- Once everything is up look for the `{"Timestamp":"2022-03-02T17:08:07.4318746+00:00","Level":"Information","MessageTemplate":"Services Configured!","Properties":{"Application":"TicketType.Microservice.Template"}}` log message.
- Using Docker Desktop terminal connect to the localstack instance.
- *NOTE: this is `read-write` access so you can modify the scripts real-time in the instance. Those changes will be lost when the instance is stopped.
- Once in, `cd /docker-entrypoint-initaws.d/tools`
- To send a predtermined Job Start message run `./incoming-publish-tool.sh`
- See the `scripts/local/localstack/tools/incoming-publish-tool.sh` file if you want to customize the message.
- There is also an `./outgoing-publish-tool.sh`
