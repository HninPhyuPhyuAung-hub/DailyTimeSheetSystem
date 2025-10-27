# Main Terraform configuration for three-region VPC architecture
# Regions: Singapore (ap-southeast-1), South Korea (ap-northeast-2), Japan (ap-northeast-1)

terraform {
  required_version = ">= 1.0"
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

# Provider for Singapore region
provider "aws" {
  alias  = "singapore"
  region = "ap-southeast-1"
}

# Provider for South Korea region
provider "aws" {
  alias  = "korea"
  region = "ap-northeast-2"
}

# Provider for Japan region
provider "aws" {
  alias  = "japan"
  region = "ap-northeast-1"
}

# Provider for Route53 (global service)
provider "aws" {
  alias  = "us-east-1"
  region = "us-east-1"
}

# Singapore VPC
module "vpc_singapore" {
  source = "./modules/vpc"
  providers = {
    aws = aws.singapore
  }

  region_name          = "singapore"
  vpc_cidr             = "10.0.0.0/16"
  public_subnet_cidrs  = ["10.0.1.0/24", "10.0.2.0/24"]
  private_subnet_cidrs = ["10.0.101.0/24", "10.0.102.0/24"]
  availability_zones   = ["ap-southeast-1a", "ap-southeast-1b"]

  tags = {
    Environment = "production"
    Region      = "Singapore"
  }
}

# South Korea VPC
module "vpc_korea" {
  source = "./modules/vpc"
  providers = {
    aws = aws.korea
  }

  region_name          = "korea"
  vpc_cidr             = "10.1.0.0/16"
  public_subnet_cidrs  = ["10.1.1.0/24", "10.1.2.0/24"]
  private_subnet_cidrs = ["10.1.101.0/24", "10.1.102.0/24"]
  availability_zones   = ["ap-northeast-2a", "ap-northeast-2c"]

  tags = {
    Environment = "production"
    Region      = "Korea"
  }
}

# Japan VPC
module "vpc_japan" {
  source = "./modules/vpc"
  providers = {
    aws = aws.japan
  }

  region_name          = "japan"
  vpc_cidr             = "10.2.0.0/16"
  public_subnet_cidrs  = ["10.2.1.0/24", "10.2.2.0/24"]
  private_subnet_cidrs = ["10.2.101.0/24", "10.2.102.0/24"]
  availability_zones   = ["ap-northeast-1a", "ap-northeast-1c"]

  tags = {
    Environment = "production"
    Region      = "Japan"
  }
}
