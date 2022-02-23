// var.deployable is set in scripts/octo/plan.sh & delpoy.sh
resource "helm_release" "ticket_type_microservice_prototype" {
  chart           = "../../helm/${var.deployable}"
  name            = local.normalized_deploy_name
  namespace       = var.TARGET_NAMESPACE
  lint            = true
  cleanup_on_fail = true

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
    value = var.K8S_SERVICEACCOUNT
  }

  set {
    name  = "serviceAccount.roleArn"
    value = aws_iam_role.application_policy.arn
  }

  set {
    name  = "global.namespaceName"
    value = var.TARGET_NAMESPACE
  }

  set {
    name  = "global.service.name"
    value = local.normalized_deploy_name
  }

  // Environment vars
  set {
    name  = "envVars.awsRegion"
    value = var.AWS_DEFAULT_REGION
  }

  set {
    name  = "envVars.SQS.QueueUrl"
    value = aws_sqs_queue.incoming_entity_queue.id
  }

  set {
    name  = "envVars.SNS.OutgoingTopicName"
    value = var.TICKETING_API_OUTGOING_TOPIC_NAME
  }

  set {
    name  = "envVars.SNS.OutgoingTopicArn"
    value = aws_sns_topic.outgoing_exceptions_topic.arn
  }

  set {
    name  = "envVars.launchDarkly.Key"
    value = var.LAUNCH_DARKLY_KEY
  }

  set {
    name  = "envVars.launchDarkly.UserName"
    value = var.LAUNCH_DARKLY_USER
  }

  set {
    name  = "envVars.epsagonToken"
    value = var.EPSAGON_TOKEN
  }

  set {
    name  = "envVars.epsagonAppName"
    value = var.EPSAGON_APP_NAME
  }
}