# Deployment Checklist

Use this checklist to ensure a successful deployment of the three-region VPC architecture.

## Pre-Deployment Checklist

### AWS Account Setup
- [ ] AWS Account created and active
- [ ] IAM user created with appropriate permissions
- [ ] Access Key ID and Secret Access Key generated
- [ ] AWS CLI installed on local machine
- [ ] AWS credentials configured (`aws configure`)
- [ ] Verified credentials work (`aws sts get-caller-identity`)

### Required Permissions
Ensure IAM user/role has these permissions:
- [ ] `ec2:*` (VPC, Subnets, Security Groups, etc.)
- [ ] `route53:*` (if using Route53 - optional)
- [ ] Access to regions: ap-southeast-1, ap-northeast-1, ap-northeast-2

### Tools Installation
- [ ] Terraform >= 1.0 installed
- [ ] Git installed
- [ ] Text editor/IDE available
- [ ] Terminal/Command prompt access

### Repository Setup
- [ ] Repository cloned locally
- [ ] Navigated to `terraform/` directory
- [ ] Reviewed README.md
- [ ] Reviewed ARCHITECTURE.md
- [ ] Reviewed QUICKSTART.md

## Deployment Checklist

### Step 1: Configuration Review
- [ ] Reviewed `main.tf` for region configurations
- [ ] Reviewed CIDR blocks (10.0.0.0/16, 10.1.0.0/16, 10.2.0.0/16)
- [ ] Verified availability zones for each region
- [ ] Checked subnet configurations (2 public, 2 private per region)
- [ ] Reviewed security group rules
- [ ] Confirmed tag values are appropriate

### Step 2: Variable Configuration (Optional)
- [ ] Created `terraform.tfvars` from `terraform.tfvars.example`
- [ ] Set project name
- [ ] Set environment name
- [ ] Configured NAT Gateway settings
- [ ] Set domain name (if using Route53)

### Step 3: Terraform Initialization
- [ ] Ran `terraform init`
- [ ] Verified successful initialization
- [ ] Checked provider download
- [ ] Confirmed module initialization
- [ ] Reviewed `.terraform.lock.hcl` file created

### Step 4: Validation
- [ ] Ran `terraform validate`
- [ ] Fixed any validation errors
- [ ] Ran `terraform fmt` to format files
- [ ] Reviewed formatted files

### Step 5: Planning
- [ ] Ran `terraform plan`
- [ ] Reviewed plan output carefully
- [ ] Verified resource counts:
  - [ ] 3 VPCs
  - [ ] 6 Public Subnets
  - [ ] 6 Private Subnets
  - [ ] 3 Internet Gateways
  - [ ] 3 NAT Gateways
  - [ ] 3 Elastic IPs
  - [ ] 6 Route Tables
  - [ ] 6 Security Groups
- [ ] Confirmed no unexpected changes
- [ ] Saved plan output for reference

### Step 6: Cost Review
- [ ] Reviewed estimated monthly costs (~$100-150)
- [ ] Confirmed budget approval
- [ ] Understood NAT Gateway costs ($32-33 per gateway/month)
- [ ] Reviewed data transfer costs
- [ ] Set up cost alerts in AWS (recommended)

### Step 7: Deployment
- [ ] Ran `terraform apply`
- [ ] Reviewed changes one final time
- [ ] Typed `yes` to confirm
- [ ] Monitored deployment progress
- [ ] Waited for completion (5-10 minutes)
- [ ] Verified successful completion message

### Step 8: Post-Deployment Verification
- [ ] Ran `terraform output`
- [ ] Noted all VPC IDs
- [ ] Noted all subnet IDs
- [ ] Noted NAT Gateway IDs
- [ ] Saved outputs to secure location

## Post-Deployment Verification Checklist

### AWS Console Verification

#### Singapore Region (ap-southeast-1)
- [ ] Navigate to AWS Console → VPC → ap-southeast-1
- [ ] Verify VPC created (10.0.0.0/16)
- [ ] Verify 2 public subnets (10.0.1.0/24, 10.0.2.0/24)
- [ ] Verify 2 private subnets (10.0.101.0/24, 10.0.102.0/24)
- [ ] Verify Internet Gateway attached
- [ ] Verify NAT Gateway in public subnet
- [ ] Verify Elastic IP allocated
- [ ] Verify route tables configured correctly
- [ ] Verify security groups created

#### Korea Region (ap-northeast-2)
- [ ] Navigate to AWS Console → VPC → ap-northeast-2
- [ ] Verify VPC created (10.1.0.0/16)
- [ ] Verify 2 public subnets (10.1.1.0/24, 10.1.2.0/24)
- [ ] Verify 2 private subnets (10.1.101.0/24, 10.1.102.0/24)
- [ ] Verify Internet Gateway attached
- [ ] Verify NAT Gateway in public subnet
- [ ] Verify Elastic IP allocated
- [ ] Verify route tables configured correctly
- [ ] Verify security groups created

#### Japan Region (ap-northeast-1)
- [ ] Navigate to AWS Console → VPC → ap-northeast-1
- [ ] Verify VPC created (10.2.0.0/16)
- [ ] Verify 2 public subnets (10.2.1.0/24, 10.2.2.0/24)
- [ ] Verify 2 private subnets (10.2.101.0/24, 10.2.102.0/24)
- [ ] Verify Internet Gateway attached
- [ ] Verify NAT Gateway in public subnet
- [ ] Verify Elastic IP allocated
- [ ] Verify route tables configured correctly
- [ ] Verify security groups created

### CLI Verification
```bash
# Verify VPCs
- [ ] aws ec2 describe-vpcs --region ap-southeast-1
- [ ] aws ec2 describe-vpcs --region ap-northeast-2
- [ ] aws ec2 describe-vpcs --region ap-northeast-1

# Verify Subnets
- [ ] aws ec2 describe-subnets --region ap-southeast-1
- [ ] aws ec2 describe-subnets --region ap-northeast-2
- [ ] aws ec2 describe-subnets --region ap-northeast-1

# Verify NAT Gateways
- [ ] aws ec2 describe-nat-gateways --region ap-southeast-1
- [ ] aws ec2 describe-nat-gateways --region ap-northeast-2
- [ ] aws ec2 describe-nat-gateways --region ap-northeast-1
```

## Documentation Checklist

### State Management
- [ ] Terraform state file backed up
- [ ] State file stored securely (not in public repo)
- [ ] Consider remote state (S3 + DynamoDB) for team use
- [ ] Document state location

### Architecture Documentation
- [ ] Document VPC IDs in architecture doc
- [ ] Document subnet IDs for reference
- [ ] Note Elastic IP addresses
- [ ] Update network diagrams if modified
- [ ] Document any customizations made

### Access Documentation
- [ ] Document security group rules
- [ ] Document SSH key pairs used (if any)
- [ ] Document IAM roles/users with access
- [ ] Create runbook for common operations

## Monitoring Setup Checklist

### CloudWatch
- [ ] Enable VPC Flow Logs for each VPC
- [ ] Set up CloudWatch Log Groups
- [ ] Create CloudWatch dashboards for monitoring
- [ ] Set up alarms for NAT Gateway usage
- [ ] Set up alarms for network traffic anomalies

### Cost Monitoring
- [ ] Enable AWS Cost Explorer
- [ ] Set up budget alerts
- [ ] Tag resources for cost tracking
- [ ] Review costs weekly for first month

### Security Monitoring
- [ ] Enable AWS CloudTrail
- [ ] Review security group rules
- [ ] Set up Config Rules for compliance
- [ ] Enable GuardDuty (optional)

## Next Steps Checklist

### Application Deployment
- [ ] Plan application architecture
- [ ] Choose deployment method (EC2, ECS, EKS, etc.)
- [ ] Create AMI or container images
- [ ] Deploy test instances in public subnets
- [ ] Deploy application in private subnets
- [ ] Configure load balancers
- [ ] Set up auto-scaling

### Database Setup
- [ ] Choose database type (RDS, Aurora, etc.)
- [ ] Deploy in private subnets
- [ ] Configure security groups
- [ ] Set up automated backups
- [ ] Test database connectivity

### Route53 Configuration (Optional)
- [ ] Register or transfer domain
- [ ] Create hosted zone
- [ ] Uncomment Route53 configuration in `route53.tf`
- [ ] Update health check endpoints
- [ ] Configure geolocation routing
- [ ] Test DNS resolution
- [ ] Test failover

### Security Hardening
- [ ] Review and restrict security group rules
- [ ] Enable MFA for AWS accounts
- [ ] Implement least-privilege IAM policies
- [ ] Enable encryption at rest
- [ ] Enable encryption in transit
- [ ] Regular security audits

### Backup and Disaster Recovery
- [ ] Set up automated snapshots
- [ ] Test restore procedures
- [ ] Document recovery procedures
- [ ] Set up cross-region replication (if needed)
- [ ] Create disaster recovery plan

## Maintenance Checklist

### Weekly
- [ ] Review AWS costs
- [ ] Check CloudWatch alerts
- [ ] Review VPC Flow Logs
- [ ] Monitor resource utilization

### Monthly
- [ ] Update Terraform to latest version
- [ ] Update AWS provider version
- [ ] Review and update security groups
- [ ] Review IAM permissions
- [ ] Check for AWS service updates

### Quarterly
- [ ] Full security audit
- [ ] Disaster recovery drill
- [ ] Review architecture for optimization
- [ ] Update documentation

## Troubleshooting Checklist

### If Deployment Fails
- [ ] Check AWS credentials
- [ ] Verify IAM permissions
- [ ] Check service quotas/limits
- [ ] Review Terraform error messages
- [ ] Check CloudTrail for API errors
- [ ] Try destroying and redeploying

### If Resources Not Created
- [ ] Check region selection
- [ ] Verify CIDR block availability
- [ ] Check availability zone availability
- [ ] Review Terraform state
- [ ] Check for conflicting resources

### If High Costs Observed
- [ ] Check data transfer usage
- [ ] Review NAT Gateway usage
- [ ] Check for idle resources
- [ ] Review Elastic IP usage
- [ ] Consider cost optimization strategies

## Cleanup Checklist

### Before Destroying
- [ ] Backup any important data
- [ ] Document current configuration
- [ ] Export any logs or metrics
- [ ] Notify stakeholders
- [ ] Remove any dependencies

### Destruction Process
- [ ] Run `terraform plan -destroy`
- [ ] Review resources to be destroyed
- [ ] Run `terraform destroy`
- [ ] Confirm destruction
- [ ] Wait for completion
- [ ] Verify all resources removed

### Post-Destruction
- [ ] Verify no resources left in AWS Console
- [ ] Check for orphaned Elastic IPs
- [ ] Verify no charges accumulating
- [ ] Clean up local Terraform files (if needed)
- [ ] Update documentation

---

## Checklist Completed
- [ ] All pre-deployment tasks completed
- [ ] Deployment successful
- [ ] Post-deployment verification passed
- [ ] Monitoring configured
- [ ] Documentation updated
- [ ] Team notified

**Deployment Date**: _______________  
**Deployed By**: _______________  
**Verified By**: _______________  

---

**Notes**:
_Use this section to document any issues, customizations, or important information about your specific deployment._

