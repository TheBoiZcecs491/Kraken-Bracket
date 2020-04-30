import axios from "axios";

const apiClient = axios.create({
  baseURL: `https://localhost:5001`,
  withCredentials: false,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
    "Access-Control-Allow-Origin": "*"
  }
});

export default {
  searchBrackets(search) {   //, pageNum, skipPage) {
    return apiClient.get("api/search/brackets/" + search);
  },
  searchEvents(search) {   //, pageNum, skipPage) {
    return apiClient.get("api/search/events/" + search);
  },
  searchGamers(search) {   //, pageNum, skipPage) {
    return apiClient.get("api/search/gamers/" + search);
  }
};