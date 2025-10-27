# Variables for the three-region VPC architecture

variable "project_name" {
  description = "Name of the project"
  type        = string
  default     = "multi-region-vpc"
}

variable "environment" {
  description = "Environment name"
  type        = string
  default     = "production"
}

variable "enable_nat_gateway" {
  description = "Enable NAT Gateway for private subnets"
  type        = bool
  default     = true
}

variable "enable_vpn_gateway" {
  description = "Enable VPN Gateway"
  type        = bool
  default     = false
}

variable "domain_name" {
  description = "Domain name for Route53 hosted zone"
  type        = string
  default     = ""
}
