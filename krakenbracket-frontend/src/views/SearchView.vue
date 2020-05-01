<template>
    <div>
        <v-container fluid>
            <v-row>
                <v-col cols="12" sm="3">
                    <select class="primary" v-model="keyword">
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
        <h1>Search Results</h1>
        <Span>{{search}} {{keyword}}</Span>
        <!-- the :event sends each prop to the Bracket component -->
        <div v-if="keyword === 'bracket'">
            <BracketModel
                v-for="bracket in brackets"
                :key="bracket.id"
                :bracket="bracket"
            />
        </div>
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
        <div v-if="keyword === 'bracket'">
            <SearchTable 
                :headers="headers" 
                :rows="desserts" 
                :keyword="keyword"/>
        </div>
        <!-- <nav class='pagination'>
            <button class='pagination-previous' @click="movePage(pagination.pageNum-1)" :disabled="isPreviousButtonDisabled">Previous</button>
            <ul class='pagination-list'>
                <li v-for="i in Math.ceil(storeResultsTotalNum/pagination.resultsPerPage)" :key=i>
                    <a class='pagination-link' ref="paginations" @click="movePage(i)">{{i}}</a>
                </li>
            </ul>
            <button class='pagination-next' @click="movePage(pagination.pageNum+1)" :disabled="isNextButtonDisabled">Next</button>
        </nav> -->
        <div v-if="noResults">
            <Span> No keyword found </Span>
        </div>
    </div>
</template>

<script>
import SearchService from "@/services/SearchService.js";
import BracketModel from "@/components/BracketModel.vue";
import GamerModel from "@/components/GamerModel.vue";
import EventCard from "@/components/EventCard.vue";
import SearchTable from "@/components/SearchTable.vue";

export default {
    components: {
        BracketModel,
        GamerModel,
        EventCard,
        SearchTable
    },
    data() {
        return {
            search: '',
            filter: '',
            keyword: 'bracket',
            options: [
                {text: 'Brackets', value: 'bracket'},
                {text: 'Events', value: 'event'},
                {text: 'Gamers', value: 'gamer'}
            ],
            brackets: [],
            gamers: [],
            events: [],
            pagination: {
                pageNum: 0,
                skipPage: 0,
                resultsPerPage: 10,
            },
            headers: [
                {
                    text: 'Dessert (100g serving)',
                    align: 'start',
                    sortable: false,
                    value: 'name',
                },
                { text: 'Calories', value: 'calories' },
                { text: 'Fat (g)', value: 'fat' },
                { text: 'Carbs (g)', value: 'carbs' },
                { text: 'Protein (g)', value: 'protein' },
                { text: 'Iron (%)', value: 'iron' },
                ],
            desserts: [
                {
                    name: 'Frozen Yogurt',
                    calories: 159,
                    fat: 6.0,
                    carbs: 24,
                    protein: 4.0,
                    iron: '1%',
                },
                {
                    name: 'Ice cream sandwich',
                    calories: 237,
                    fat: 9.0,
                    carbs: 37,
                    protein: 4.3,
                    iron: '1%',
                },
                {
                    name: 'Eclair',
                    calories: 262,
                    fat: 16.0,
                    carbs: 23,
                    protein: 6.0,
                    iron: '7%',
                },
                {
                    name: 'Cupcake',
                    calories: 305,
                    fat: 3.7,
                    carbs: 67,
                    protein: 4.3,
                    iron: '8%',
                },
                {
                    name: 'Gingerbread',
                    calories: 356,
                    fat: 16.0,
                    carbs: 49,
                    protein: 3.9,
                    iron: '16%',
                },
                {
                    name: 'Jelly bean',
                    calories: 375,
                    fat: 0.0,
                    carbs: 94,
                    protein: 0.0,
                    iron: '0%',
                },
                {
                    name: 'Lollipop',
                    calories: 392,
                    fat: 0.2,
                    carbs: 98,
                    protein: 0,
                    iron: '2%',
                },
                {
                    name: 'Honeycomb',
                    calories: 408,
                    fat: 3.2,
                    carbs: 87,
                    protein: 6.5,
                    iron: '45%',
                },
                {
                    name: 'Donut',
                    calories: 452,
                    fat: 25.0,
                    carbs: 51,
                    protein: 4.9,
                    iron: '22%',
                },
                {
                    name: 'KitKat',
                    calories: 518,
                    fat: 26.0,
                    carbs: 65,
                    protein: 7,
                    iron: '6%',
                },
                {
                    name: 'notKitKat',
                    calories: 518,
                    fat: 26.0,
                    carbs: 65,
                    protein: 7,
                    iron: '6%',
                },
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
    computed: {
        noResults: function() {
            if(!this.brackets.length && !this.events.length && !this.gamers.length){
                return true;
            }
            else{
                return false;
            }
        },
        isPreviousButtonDisabled: function(){
            if(this.currentPage === 1){
                return true;
            }
            return false;
        },
        isNextButtonDisabled: function(){
            if(this.currentPage === Math.ceil(this.storeResultsTotalNum/this.resultsPerPage)){
                return true;
            }
            return false;
        },
    }
};
</script>
