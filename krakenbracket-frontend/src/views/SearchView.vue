<template>
  <div>
    <SearchBar></SearchBar>
    <h1>Search Results</h1>
    <Span>{{search}} {{keyword}}</Span>
    <!-- the :event sends each prop to the Bracket component -->
    <!-- v-if statment -->
    <!-- <div v-if="keyword === 'bracket'"> -->
    <BracketModel
      v-for="bracket in brackets"
      :key="bracket.id"
      :bracket="bracket"
    />
    <!-- </div> -->
    <div v-if="keyword === 'event'">
    <EventCard
        v-for="event in events"
        :key="event.id"
        :event="event"
    />
    </div>
    <div v-if="keyword === 'gamer'">
    <GamerModel
        v-for="gamer in gamers"
        :key="gamer.hashedUserID"
        :gamer="gamer"
    />
    </div>
  </div>
</template>

<script>
import SearchService from "@/services/SearchService.js";
import SearchBar from "@/components/SearchBar.vue";
import BracketModel from "@/components/BracketModel.vue";
import GamerModel from "@/components/GamerModel.vue";
import EventCard from "@/components/EventCard.vue";

export default {
    static: {
        visibleItemsPerPageCount: 10
    },
    props: {
        search:{
            type: String,
            required: true
        },
        keyword: {
            type: String,
            required: true
        }
    },
    components: {
        BracketModel,
        GamerModel,
        EventCard,
        SearchBar
    },
    // watch: {
    //     search: function(newVal, oldVal){
    //         brackets = [],
    //         gamers = [],
    //         events = []
    //     }
    // },
    data() {
        return {
            brackets: [],
            gamers: [],
            events: [],
            // pagination: {
            //     pageNum: 1,
            //     skipPage: 0,
            //     visibleCount: 10,
            // }
            // pageNum: 1,
            // skipPage: 0,
        };
    },
    // methods: {
    //     nextPage() {
    //         t
    //     }
    // },
    created() {
        if(this.keyword === "bracket"){
            console.log(true);
            SearchService.searchBrackets(this.search) // this.pageNum, this.skipPage)
            .then(response => {
                this.brackets = response.data;
                //console.log(response.data)
            })
            .catch(error => {
                console.log("There was an error: " + error);
            });
        } else if(this.keyword === "event"){
            console.log(true);            
            SearchService.searchEvents(this.search) // this.pageNum, this.skipPage)
            .then(response => {
                this.brackets = response.data;
                //console.log(response.data)
            })
            .catch(error => {
                console.log("There was an error: " + error);
            });
        } else if(this.keyword === "gamer"){
            console.log(true);
            SearchService.searchGamers(this.search) // this.pageNum, this.skipPage)
            .then(response => {
                this.brackets = response.data;
                //console.log(response.data)
            })
            .catch(error => {
                console.log("There was an error: " + error);
            });
        }
    },
};
</script>
