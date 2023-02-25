/* groovylint-disable-next-line CompileStatic */
pipeline {
    agent any
    stages {
        stage('Checkout Stage') {
            steps {
                git url: 'https://github.com/hemersonlcosta/EagleRockAPI.git', branch: 'main'
            }
        }
        stage('Build Stage') {
            steps {
                script {
                        dockerapp = docker.build("hemersonlcosta/eaglerockapi:latest",
                            '-f Dockerfile .')
                }
            }
        }
    }
}
