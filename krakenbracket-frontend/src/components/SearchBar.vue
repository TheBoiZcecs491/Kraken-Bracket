<template>
    <div>
        <v-container fluid>
            <v-row
            align="start"
            justify="center">
                <v-col cols="12" sm="2">
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
                <v-col cols="12" sm="8">
                    <v-text-field
                        class="search-input"
                        v-model="search"
                        type="search"
                        placeholder="Search..."
                        required
                        clearable
                    >
                    </v-text-field>
                </v-col>
                <v-col cols="12" sm="2">
                <router-link
                    :to="{ name:'search-view', params: {search: search, keyword: keyword} }"
                >
                    <v-btn class="primary" color="#2196F3">Search</v-btn>
                </router-link>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script>
export default {
    name: "SearchBar",
    data() {
        return {
            keyword: 'bracket',
            search: '',
            options: [
                {text: 'Brackets', value: 'bracket'},
                {text: 'Events', value: 'event'},
                {text: 'Gamers', value: 'gamer'}
            ]
        }
    },
}
</script>