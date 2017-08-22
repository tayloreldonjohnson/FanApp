class SecretController {
    constructor($http) {
        this.secrets = {};
        this.http = $http;
    }

    getSecrets() {
        this.http.get('/api/secrets')
            .then((results) => {
                this.secrets = results.data;
            });
    }
}