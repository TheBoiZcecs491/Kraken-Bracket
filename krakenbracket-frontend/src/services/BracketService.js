
import axios from 'axios'
    
    const apiClient = axios.create({  
      baseURL: `http://localhost:44352/`,
      withCredentials: false, // This is the default
      headers: {
        Accept: "application/json",
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*"
  }
});

export default {
  getBrackets() {
    return apiClient.get("api/brackets");
  },
  getBracketByID(bracketID) {
    return apiClient.get("api/brackets/" + bracketID);
  }
};