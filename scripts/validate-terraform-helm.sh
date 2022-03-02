#!/bin/bash

PROJECT=TicketType.Microservice.Template

printf "\n#####################################################"
printf "\nThis will validate both the Helm & Terraform configs."
printf "\nYou will be prompted for respective choices."
printf "\n\nThe Helm part will do a dry-run installation but will require that"
printf "\nyou be logged in to AWS SSO in the dev environment."
printf "\nIf you have your AWS profiles configured you should be able to run:"
printf "\n\naws sso login --profile dev"
printf "\n\nContinue? [Y|n]:"
read CONTINUE

if [[ -z $CONTINUE ]]
then
  CONTINUE=Y
fi

if [[ $CONTINUE != 'Y' ]]
then
  printf "L8tr Sk8tr!\n"
  exit 0
fi

printf "\nRun terraform init? [N|y]:"
read CONTINUE

if [[ -z $CONTINUE ]]
then
  CONTINUE=N
fi

cd terraform/$PROJECT

if [[ $CONTINUE == 'y' ]]
then
  printf "\nterraform init -backend=false\n"
  terraform init -backend=false
fi

printf "\nValidate Terraform config? [Y|n]:"
read CONTINUE

if [[ -z $CONTINUE ]]
then
  CONTINUE=Y
fi

if [[ $CONTINUE == 'Y' ]]
then
  printf "\n\nValidating local Terraform config..."
  printf "\nterraform validate\n"
  terraform validate
fi

printf "\nDo a Helm install dry run? [Y|n]:"
read CONTINUE

if [[ -z $CONTINUE ]]
then
  CONTINUE=Y
fi

if [[ $CONTINUE != 'Y' ]]
then
  printf "L8tr Sk8tr!\n"
  exit 0
fi
  
printf "Doing a dry run of Helm install...\n"
cd ../..

printf "helm install --dry-run ticket-type-microservice-template helm/%s\n" $PROJECT
helm install --dry-run ticket-type-microservice-template helm/$PROJECT

exit $?
