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
  searchBrackets(search) {
    return apiClient.get("api/search/brackets/" + search);
  },
  searchEvents(search) {
    return apiClient.get("api/search/events/" + search);
  },
  searchGamers(search) {
    return apiClient.get("api/search/gamers/" + search);
  }
};
