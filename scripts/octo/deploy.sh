#!/usr/bin/env bash
set -e

# shellcheck disable=SC1091
source SetAWScredentials.sh
source SetWorkSpace.sh

REVISION=$(get_octopusvariable "Octopus.Release.Number")
ENVIRONMENT=$(get_octopusvariable "Octopus.Environment.Name")
DEPLOYABLE="TicketType.Microservice.Template"
ECR_REPO_URL=$(get_octopusvariable "ECR_REPO_URL")

: "$REVISION"

export TF_VAR_deployable=${DEPLOYABLE}
# shellcheck disable=SC2034
export TF_VAR_image_tag="${ECR_REPO_URL}:${REVISION}"
# shellcheck disable=SC2155
export TF_VAR_oidc_provider=$(aws eks describe-cluster --name "${CLUSTER_NAME}" --region "${CLUSTER_REGION}" --query "cluster.identity.oidc.issuer" --output text | sed -e "s/^https:\/\///")
export TF_VAR_revision=${REVISION}
export TF_VAR_environment="eng"

aws eks update-kubeconfig --name "${CLUSTER_NAME}" --region "${CLUSTER_REGION}"

helm dependency update ../../helm/"$DEPLOYABLE"/
cd ../../terraform/"$DEPLOYABLE"/
terraform init -backend-config "environment/octo.tfconfig"
terraform workspace select "eng" || terraform workspace new "eng"
terraform plan -refresh \
  -var-file "environment/octo.tfvars.json" \
  -var "revision=${REVISION}" \
  -input=false -no-color \
  -out=./tfplan
terraform show -no-color ./tfplan
terraform apply -input=false -no-color ./tfplan
