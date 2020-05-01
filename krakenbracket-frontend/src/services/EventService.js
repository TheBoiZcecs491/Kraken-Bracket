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
  getEventByID(eventID) {
    return apiClient.get("api/events/" + eventID);
  },
  getBracketPlayerInfo(email) {
    return apiClient.get(`api/brackets/${email}/bracketPlayerInfo`);
  }
};