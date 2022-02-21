#!/bin/bash

PROJECT=TicketType.Microservice.Template

printf "Validating local Terraform config...\n"

cd terraform/$PROJECT

terraform init -backend=false
terraform validate

printf "Do a dry run of Helm install...\n"

cd ../..

helm install --dry-run $PROJECT helm/$PROJECT

exit $?
