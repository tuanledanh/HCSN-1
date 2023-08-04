import axios from 'axios'

const http = axios.create({
    baseURL: 'https://localhost:7078/api/v1/',
    timeout: 5000
})

export default http
