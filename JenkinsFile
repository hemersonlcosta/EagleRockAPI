pipeline {
    agent any
    environment {
        dotnet = 'C:\\Program Files\\dotnet\\dotnet.exe'
    }
    stages {
        stage('Checkout Stage') {
            steps {
                git url: 'https://github.com/Jaydeep-007/JenkinsWebApplicationDemo.git', branch: 'main'
            }
        }
        stage('Build Stage') {
            steps {
                script {
                        dockerapp = docker.build("hemersonlcosta/EagleRockAPI:{$env.BUILD_ID}", '-f ./Dockerfile .')
                }
            }
        }
    }
}