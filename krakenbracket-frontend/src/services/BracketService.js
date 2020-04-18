
import axios from 'axios'
    
    const apiClient = axios.create({  
      baseURL: `http://localhost:8080/`,
      withCredentials: false, // This is the default
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      }
    })
    
    export default {
      getBrackets() {
        return apiClient.get('/brackets')
      }
    }