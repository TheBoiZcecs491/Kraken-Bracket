<template>
  <div>
    <v-container fluid>
      <v-row>
        <v-col cols="12" sm="3">
          <select class="search-type" v-model="keyword" @change="updateModel">
            <option
              v-for="option in options"
              v-bind:key="option.text"
              v-bind:value="option.value"
            >
              {{ option.text }}
            </option>
          </select>
        </v-col>
        <v-col cols="12" sm="6">
          <v-text-field
            class="search-input"
            v-model="search"
            type="search"
            placeholder="Search..."
            append-icon="mdi-magnify"
            required
            clearable
            @keyup.enter="updateModel"
          >
          </v-text-field>
        </v-col>
        <v-col cols="12" sm="3">
          <v-btn class="primary" color="#2196F3" @click="updateModel"
            >Search</v-btn
          >
        </v-col>
      </v-row>
    </v-container>
    <div v-if="isSearch">
      <SearchTable :headers="header" :search_result="model" />
    </div>
  </div>
</template>

<script>
import SearchService from "@/services/SearchService.js";
import SearchTable from "@/components/SearchTable.vue";

export default {
  // This component is used to display the results.
  components: {
    SearchTable
  },
  data() {
    return {
      // Search data
      isSearch: false,
      search: "",
      // Search selector data
      keyword: "bracket",
      options: [
        { text: "Brackets", value: "bracket" },
        { text: "Events", value: "event" },
        { text: "Gamers", value: "gamer" }
      ],
      // Search result data
      model: [],
      header: [],
      bracketHeaders: [
        { text: "Bracket Name", value: "bracketName" },
        { text: "Gaming Platform", value: "gamingPlatform" },
        { text: "Game", value: "gamePlayed" },
        { text: "Start Date", value: "startDate" },
        { text: "End Date", value: "endDate" },
        { text: "Number of Players", value: "playerCount" }
      ],
      eventHeaders: [
        { text: "Event Name", value: "eventName" },
        { text: "Start Date", value: "startDate" },
        { text: "End Date", value: "endDate" },
        { text: "Location", value: "address" }
      ],
      gamerHeaders: [{ text: "Gamer Tag", value: "gamerTag" }]
    };
  },
  methods: {
    // This method is used to fetch data and update the model
    updateModel() {
      var response;
      if (this.keyword === "bracket") {
        response = SearchService.searchBrackets(this.search);
        this.header = Array.from(this.bracketHeaders);
      } else if (this.keyword === "event") {
        response = SearchService.searchEvents(this.search);
        this.header = Array.from(this.eventHeaders);
      } else if (this.keyword === "gamer") {
        response = SearchService.searchGamers(this.search);
        this.header = Array.from(this.gamerHeaders);
      }
      response
        .then(r => {
          this.model = r.data;
        })
        .catch(error => {
          console.log("There was an error: " + error);
        });
      if (this.search) {
        this.isSearch = true;
      } else {
        this.isSearch = false;
      }
    }
  }
};
</script>
