<<<<<<< HEAD
import axios from "axios";

const apiClient = axios.create({
  baseURL: `https://localhost:44352/api`,
  withCredentials: false, // This is the default
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json"
  }
});

export default {
  getBrackets() {
    return apiClient.get("/brackets");
  },
  getBracketByID(bracketID) {
    return apiClient.get("/brackets/" + bracketID);
  }
};
=======
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
>>>>>>> parent of 1f5a6a7... Bracket Info Displayed (via URL)
