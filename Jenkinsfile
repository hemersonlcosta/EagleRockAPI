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
        // stage('Push Stage') {
        //     steps {
        //         script {
        //                 docker.withRegistry('https://registry.hub.docker.com', 'dockerhub'){
        //                     dockerapp.push('lastest')
        //                 }
        //         }
        //     }
        // }
        stage('Deploy Stage') {
            steps {
                sh 'envsubst < k8s/*.yaml | kubectl apply -f -'
            }
        }
    }
}

