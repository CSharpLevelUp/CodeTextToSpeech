terraform {
    source = "tfr://registry.terraform.io/terraform-aws-modules/ec2-instance/aws//?version=5.6.1"
}

include "root" {
  path = find_in_parent_folders()
  expose = true
}

dependency "vpc" {
  config_path = "../vpc"

  mock_outputs_allowed_terraform_commands = ["validate", "plan"]
  mock_outputs = {
      public_subnets = ["subnet-01", "subnet-02"]
      vpc_id = "vpc-mock-id"
  }
}

dependency "eb_env" {
  config_path = "../beanstalk-environment"

  mock_outputs_allowed_terraform_commands = ["validate", "plan"]
  mock_outputs = {
      security_group_id = "sg-id"
  }
}

/*
  Remember to set up gh runner after setting up this instance:
*/
inputs = {
    name = "gh-runner-codetexttospeech"
    key_name = "csharp-level-up"
    ami_ssm_parameter = "/aws/service/canonical/ubuntu/server/20.04/stable/current/amd64/hvm/ebs-gp2/ami-id"
    vpc_security_group_ids = [dependency.eb_env.outputs.security_group_id]
    subnet_id = dependency.vpc.outputs.public_subnets[0]
    associate_public_ip_address  = true
}