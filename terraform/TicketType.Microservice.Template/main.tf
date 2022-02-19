terraform {
  backend "s3" {}
  required_providers {
    aws = {
      version = "~> 4.1"
    }
    helm = {
      version = "~> 2.4"
    }
    kubernetes = {
      version = "~> 2.8"
    }    
  }
}

provider "aws" {
  region = var.aws_default_region
  default_tags {
    tags = module.tags.tags
  }
}

provider "helm" {}

provider "kubernetes" {}

data "aws_caller_identity" "current" {}

data "aws_kms_alias" "sns_key" {
  name = var.kms_key_alias_sns
}

locals {
  aws_account_id    = data.aws_caller_identity.current.account_id
  project_name      = "exception-recognition-engine"
  deployable        = "ticket-type-microservice-prototype-v1"
  env_deployable    = "${var.environment}-${local.project_name}-${local.deployable}"
  env_name          = "${var.environment}-${local.deployable}"
  legacy_deployable = "TicketType.Microservice.Template"
  kebab_name        = replace(lower(local.legacy_deployable), ".", "-")
  kebab_env_name    = "${var.environment}-${local.kebab_name}"
  aws_policy_version = "2012-10-17"
  sns_ticketing_topic_name = "${var.environment}-${var.ticketing_api_outgoing_topic}"
  sns_incoming_topic_name = "${var.environment}-${var.entity_api_incoming_topic}"
  sns_incoming_topic_data_name = "${var.environment}-${var.entity_api_incoming_topic}-data"
  sqs_incoming_queue_name = "${var.environment}-${var.entity_api_incoming_queue}"
}