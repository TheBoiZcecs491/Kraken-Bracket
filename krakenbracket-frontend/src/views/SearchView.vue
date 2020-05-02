<template>
    <div>
        <v-container fluid>
            <v-row>
                <v-col cols="12" sm="3">
                    <select class="search-type" v-model="keyword">
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
                    >
                    </v-text-field>
                </v-col>
                <v-col cols="12" sm="3">
                    <v-btn class="primary" color="#2196F3" @click="updateModel">Search</v-btn>
                </v-col>
            </v-row>
        </v-container>
        <div v-if="!noSearch">
            <div v-if="keyword === 'bracket'">
                <SearchTable 
                    :headers="bracketHeaders" 
                    :search_result="brackets"/>
            </div>
            <div v-if="keyword === 'event'">
                <SearchTable 
                    :headers="eventHeaders" 
                    :search_result="events"/>
            </div>
            <div v-if="keyword === 'gamer'">
                <SearchTable 
                    :headers="gamerHeaders" 
                    :search_result="gamers"/>
            </div>
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
            noSearch: true,
            search: '',
            // Search selector data
            keyword: 'bracket',
            options: [
                {text: 'Brackets', value: 'bracket'},
                {text: 'Events', value: 'event'},
                {text: 'Gamers', value: 'gamer'}
            ],
            // Search result data
            brackets: [],
            gamers: [],
            events: [],
            bracketHeaders: [
                { text: 'Bracket Name', value: 'bracketName' },
                { text: 'Gaming Platform', value: 'gamingPlatform'},
                { text: 'Game', value: 'gamePlayed'},
                { text: 'Start Date', value: 'startDate' },
                { text: 'End Date', value: 'endDate'},
                { text: 'Number of Players', value: 'playerCount'}
            ],
            eventHeaders: [
                { text: 'Event Name', value: 'eventName' },
                { text: 'Start Date', value: 'startDate' },
                { text: 'End Date', value: 'endDate'},
                { text: 'Location', value: 'address'}
            ],
            gamerHeaders: [
                { text: 'Gamer Tag', value: 'gamerTag' },
            ],
        };
    },
    methods: {
        // This method is used to fetch data and update the model
        updateModel() {
            if(this.keyword === "bracket"){
                SearchService.searchBrackets(this.search)
                .then(response => {
                    this.brackets = response.data;
                })
                .catch(error => {
                    console.log("There was an error: " + error);
                });
            } else if(this.keyword === "event"){          
                SearchService.searchEvents(this.search)
                .then(response => {
                    this.events = response.data;
                })
                .catch(error => {
                    console.log("There was an error: " + error);
                });
            } else if(this.keyword === "gamer"){
                SearchService.searchGamers(this.search)
                .then(response => {
                    this.gamers = response.data;
                })
                .catch(error => {
                    console.log("There was an error: " + error);
                });
            }
            if (this.search === ""){
                this.noSearch = true;
            } else{
                this.noSearch = false;
            }
        },
    },
};
</script>
