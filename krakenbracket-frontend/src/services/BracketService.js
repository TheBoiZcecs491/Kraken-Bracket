import axios from "axios";

const apiClient = axios.create({
  baseURL: "http://localhost:44352",
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
  getEvents() {
    return apiClient.get("/events");
  },
  getEventByID(eventID) {
    return apiClient.get("api/events/" + eventID);
  }
};
