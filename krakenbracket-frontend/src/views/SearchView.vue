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
        <!-- the :event sends each prop to the Bracket component -->
        <div v-if="keyword === 'bracket'">
            <SearchTable 
                :headers="bracketHeaders" 
                :rows="brackets"/>
        </div>
        <div v-if="keyword === 'event'">
            <SearchTable 
                :headers="eventHeaders" 
                :rows="events"/>
        </div>
        <div v-if="keyword === 'gamer'">
            <SearchTable 
                :headers="gamerHeaders" 
                :rows="gamers"/>
        </div>
    </div>
</template>

<script>
import SearchService from "@/services/SearchService.js";
import SearchTable from "@/components/SearchTable.vue";

export default {
    components: {
        SearchTable
    },
    data() {
        return {
            search: '',
            keyword: 'bracket',
            options: [
                {text: 'Brackets', value: 'bracket'},
                {text: 'Events', value: 'event'},
                {text: 'Gamers', value: 'gamer'}
            ],
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
        updateModel() {
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
                    this.events = response.data;
                    //console.log(response.data)
                })
                .catch(error => {
                    console.log("There was an error: " + error);
                });
            } else if(this.keyword === "gamer"){
                console.log(true);
                SearchService.searchGamers(this.search) // this.pageNum, this.skipPage)
                .then(response => {
                    this.gamers = response.data;
                    //console.log(response.data)
                })
                .catch(error => {
                    console.log("There was an error: " + error);
                });
            }
        },
    },
};
</script>
