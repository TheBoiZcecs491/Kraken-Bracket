import axios from 'axios'
    
    const apiClient = axios.create({  
      baseURL: `https://localhost:44352/api`,
      withCredentials: false, // This is the default
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      }
    })
    
    export default {
      getBrackets() {
        return apiClient.get('/brackets')
      },
      getBracketByID(bracketID){
        return apiClient.get('/brackets/' + bracketID)
      }
    }