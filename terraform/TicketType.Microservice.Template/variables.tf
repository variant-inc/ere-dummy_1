// Variant Settings
// The following values come from deploy.sh & plan.sh
// in scripts/octo/
variable "deployable" {
  type=string
}

variable "environment" {
  type = string
}

variable "revision" {
  type = string
}

variable "image_tag" {
  type = string
}

variable "oidc_provider" {
  type = string
}
///

variable "K8S_SERVICEACCOUNT" {
  type = string
}

variable "TARGET_NAMESPACE" {
  type = string
}

variable "PROJECT_NAME" {
  type = string
}

variable "team" {
  type = string
}

variable "owner" {
  type = string
}

// AWS Settings
variable "AWS_POLICY_VERSION" {
  type = string
}

variable "AWS_DEFAULT_REGION" {
  type    = string
  default = "us-east-1"
}

// Epsagon Settings
variable "EPSAGON_APP_NAME" {
  type = string
}

variable "EPSAGON_TOKEN" {
  type        = string
  description = "Set via 'Set AWS credentials' Octopus Script Module"
}

// LaunchDarkly Settings
variable "LAUNCH_DARKLY_KEY" {
  type = string
}

variable "LAUNCH_DARKLY_USER" {
  type = string
}

//

variable "octopus_tags" {
  type = map(string)
}

// SNS/SQS
variable "ENTITY_QUEUE_RETENTION_SECONDS" {
  type = number
  default     = 604800 # 7 days
  description = "Number of seconds the queue retains messages"
}

variable "ENTITY_API_INCOMING_QUEUE_NAME" {
  type = string
}

variable "ENTITY_API_INCOMING_QUEUE_URL" {
  type = string
}

variable "TICKETING_API_OUTGOING_TOPIC_NAME" {
  type = string
}

variable "kms_key_alias_sns" {
  type    = string
  default = "alias/ops/sns"
}