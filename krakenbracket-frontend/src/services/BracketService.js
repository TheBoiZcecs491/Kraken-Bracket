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
  getBracketPlayerInfo(email) {
    return apiClient.get(`api/brackets/${email}/bracketPlayerInfo`);
  },
  getGamerInfo(email) {
    return apiClient.get(`api/brackets/${email}/gamerInfo`);
  },
  getBracketCompetitorInfo(bracketID){
    return apiClient.get(`api/brackets/${bracketID}/competitorInfo`);
  },
  updateBracketStandings(bracketID, bracketCompetitor){
    console.log("Competitor info: --- " + bracketCompetitor);
    return apiClient.post(`api/brackets/${bracketID}/competitorInfo/update/`,
    {
      bracketID: bracketCompetitor.bracketID,
      score: bracketCompetitor.score,
      placement: bracketCompetitor.placment,
      hashedUserID: bracketCompetitor.hashedUserID,
      gamerTag: bracketCompetitor.gamerTag
    })
  }
};
