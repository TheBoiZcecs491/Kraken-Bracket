import axios from "axios";

const apiClient = axios.create({
  baseURL: `https://localhost:44352`,
  withCredentials: false,
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
  },
  getBracketPlayerInfo(email){
    return apiClient.get(`api/brackets/${email}/bracketPlayerInfo`)
  }
};