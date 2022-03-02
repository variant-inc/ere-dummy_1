#!/usr/bin/env bash
set -e

# shellcheck disable=SC1091
source SetAWScredentials.sh

WORKSPACE=eng
REVISION=$(get_octopusvariable "Octopus.Release.Number")
ENVIRONMENT=$(get_octopusvariable "Octopus.Environment.Name")
DEPLOYABLE="TicketType.Microservice.Template"

: "$REVISION"

export TF_VAR_deployable=${DEPLOYABLE}
# shellcheck disable=SC2034
export TF_VAR_image_tag=${REVISION}
# shellcheck disable=SC2155
export TF_VAR_oidc_provider=$(aws eks describe-cluster --name "${CLUSTER_NAME}" --region "${CLUSTER_REGION}" --query "cluster.identity.oidc.issuer" --output text | sed -e "s/^https:\/\///")
export TF_VAR_revision=${REVISION}
export TF_VAR_environment="eng"

aws eks update-kubeconfig --name "${CLUSTER_NAME}" --region "${CLUSTER_REGION}"

cd ../../terraform/"$DEPLOYABLE"/
terraform init -backend-config "environment/octo.tfconfig"
terraform validate -no-color
terraform workspace select "eng" || terraform workspace new "eng"
terraform plan -refresh \
    -var-file "environment/octo.tfvars.json" \
    -input=false -no-color
