<template>
  <div>
    <!-- <SearchBar></SearchBar> -->
    <h1>Search Results</h1>
    <!-- the :event sends each prop to the Bracket component -->
    <!-- v-if statment -->
    <BracketModel
      v-for="bracket in brackets"
      :key="bracket.id"
      :bracket="bracket"
    />
  </div>
</template>

<script>
//import SearchBar from "@/components/SearchBar.vue";
import SearchService from "@/services/SearchService.js";
import BracketModel from "@/components/BracketModel.vue";
//import BracketService from "@/services/BracketService.js";
// GamerModel & Service
// EventModel & Service
export default {
    static: {
        visibleItemsPerPageCount: 15
    },
    props: {
        search: String,
        keyword: String
        //["search", "keyword"]
    },
    components: {
        BracketModel,
        //SearchBar
        // GamerModel
        // EventModel
    },
    data() {
        return {
            brackets: []
            // pageNum: 1,
            // skipPage: 0,
            // gamers: []
            // events: []
        };
    },
    created() {
        SearchService.getSearchBrackets(this.search) // this.pageNum, this.skipPage)
        .then(response => {
            this.brackets = response.data;
            //console.log(response.data)
        })
        .catch(error => {
            console.log("There was an error: " + error);
        });
        // .getSearchGamers
        // .getSearchEvents
    }
};
</script>