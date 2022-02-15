resource "kubernetes_namespace" "ticketing_k8_namespace" {
  metadata {
    name = var.target_namespace
    labels = {
      "istio-injection": "enabled"
    }
  }
}