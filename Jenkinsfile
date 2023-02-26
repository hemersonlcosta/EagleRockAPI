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
                        dockerapp = docker.build("hemersonlcosta/eaglerockapi:${env.BUILD_ID}",
                            '-f Dockerfile .')
                }
            }
        }
        // stage('Push Stage') {
        //     steps {
        //         script {
        //                 docker.withRegistry('https://registry.hub.docker.com', 'dockerhub'){
        //                     dockerapp.push('lastest')
        //                     dockerapp.push("${env.BUILD_ID}")
        //                 }
        //         }
        //     }
        // }
        stage('Deploy Stage') {
            environment{
                tag_version = "${env.BUILD_ID}"
            }
            steps {
                sh(script: "cat k8s/eaglerockapi.yaml | sed -i 's/{{tag}}/$tag_version/g'  k8s/eaglerockapi.yaml | kubectl apply -f - --kubeconfig /var/lib/jenkins/.kube/config", returnStdout: true)
                sh(script: "cat k8s/apiservice.yaml | kubectl apply -f - --kubeconfig /var/lib/jenkins/.kube/config", returnStdout: true)
                sh(script: "cat k8s/hpa.yaml | kubectl apply -f - --kubeconfig /var/lib/jenkins/.kube/config", returnStdout: true)
                // sh 'sed -i "s/{{tag}}/$tag_version/g"  k8s/eaglerockapi.yaml'
                // sh 'envsubst < k8s/eaglerockapi.yaml | kubectl apply -f -'
                // sh 'envsubst < k8s/apiservice.yaml | kubectl apply -f -'
                // sh 'envsubst < k8s/hpa.yaml | kubectl apply -f -'
            }
        }
    }
}

