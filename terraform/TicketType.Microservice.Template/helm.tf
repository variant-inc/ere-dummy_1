// var.deployable is set in scripts/octo/plan.sh & delpoy.sh
resource "helm_release" "ticket_type_microservice_prototype" {
  chart     = "../../helm/${var.deployable}"
  name      = local.normalized_deploy_name
  namespace = var.target_namespace
  lint      = true

  // The following are set in scripts/octo/plan.sh & delpoy.sh:
  set {
    name  = "revision"
    value = var.revision
  }

  set {
    name  = "image.tag"
    value = var.image_tag
  }
  /// End

  set {
    name  = "forceRefresh"
    value = "1.1"
  }

  set {
    name  = "fullnameOverride"
    value = local.normalized_deploy_name
  }

  set {
    name  = "serviceAccount.name"
    value = var.k8s_serviceaccount
  }

  set {
    name  = "serviceAccount.roleArn"
    value = aws_iam_role.ticket_type_microservice_template.arn
  }

  set {
    name  = "global.namespaceName"
    value = var.target_namespace
  }

  set {
    name  = "global.service.name"
    value = local.normalized_deploy_name
  }

  // Environment vars
  set {
    name  = "envVars.awsRegion"
    value = var.aws_default_region
  }

  set {
    name  = "envVars.SQS.QueueUrl"
    value = var.entity_api_incoming_queue_url
  }

  set {
    name  = "envVars.SQS.QueueName"
    value = var.entity_api_incoming_queue_name
  }

  set {
    name  = "envVars.SNS.OutgoingTopicName"
    value = var.ticketing_api_outgoing_topic_name
  }

  set {
    name  = "envVars.SNS.OutgoingTopicArn"
    value = var.ticketing_api_outgoing_topic_arn
  }

  set {
    name  = "envVars.launchDarkly.Key"
    value = var.launch_darkly_key
  }

  set {
    name  = "envVars.launchDarkly.UserName"
    value = var.launch_darkly_user
  }

  set {
    name  = "envVars.epsagonToken"
    value = var.epsagon_token
  }

  set {
    name  = "envVars.epsagonAppName"
    value = var.epsagon_app_name
  }
}