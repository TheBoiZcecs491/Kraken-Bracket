<template>
  <div class="new-event">
    <v-app id="inspire">
      <v-form
        ref="form"
        v-model="valid"
        @submit.prevent="createEvent"
      >
        <v-row justify="space-around">
          <v-col class="px-4" cols="12" sm="3">
            <v-text-field
              v-model="EventName"
              label="Event Name"
              :rules="eventNameRule"
              placeholder="Event Name of your choice"
              required
            >
            </v-text-field>

            <v-row>
              <v-col cols="12" md="15">
                <v-textarea
                v-model="EventDescription"
                label="Event Description"
                :rules="eventDescriptionRule"
                placeholder="Quick description of the Event"
                hint="700 char max"
                >
                </v-textarea>
              </v-col>
            </v-row>

            <v-container>
              <v-row>
                <v-col cols="12" lg"6">
                  <v-menu
                    v-model="menu1"
                    :close-on-content-click="false"
                    :nudge-left="-40"
                    translate="scale-transition"
                    offset-y 
                    min-width="290px"
                  >
                    <template v-slot:activator="{on}">
                      <v-text-field
                        v-model="startDate"
                        label="Start Date"
                        :rules="[values => !!value || 'Required' ]"
                        readonly
                        v-on="on"
                        required
                      >
                      </v-text-field>
                    </template>
                    <v-date-picker
                      v-model="startDate"
                      @input="menu1 = false"
                    >
                    </v-date-picker>
                  </v-menu>
                </v-col>

                <v-spacer></v-spacer>
                
                <v-col cols="12" lg"6">
                  <v-menu
                    ref="menu2"
                    v-model="menu2"
                    :close-on-content-click="false"
                    :nudge-left="40"
                    :return-value.sync="time"
                    translate="scale-transition"
                    offset-y
                    min-width="290px"
                    max-width="290px"
                  >
                    <template v-slot:activator="{on}">
                      <v-text-field
                        v-model="startTime"
                        label="Start Time"
                        :rules="[values => !!value || 'Required' ]"
                        v-on="on"
                        required
                      >
                      </v-text-field>
                    </template>
                  <v-time-picker
                    v-if="menu2"
                    v-model="startTime"
                    full-width
                  >
                  </v-time-picker>
                </v-menu>
              </v-col>
            </v-row>
          </v-container>

          <v-container>
              <v-row>
                <v-col cols="12" lg"6">
                  <v-menu
                    v-model="menu3"
                    :close-on-content-click="false"
                    :nudge-left="-40"
                    translate="scale-transition"
                    offset-y
                    min-width="290px"
                  >
                    <template v-slot:activator="{on}">
                      <v-text-field
                        v-model="endDate"
                        label="End Date"
                        :rules="[values => !!value || 'Required' ]"
                        readonly
                        v-on="on"
                        required
                      >
                      </v-text-field>
                    </template>
                    <v-date-picker
                      v-model="endDate"
                      @input="menu3 = false"
                    >
                    </v-date-picker>
                  </v-menu>
                </v-col>

                <v-spacer></v-spacer>

                <v-col cols="12" lg"6">
                    <v-menu
                      ref="menu4"
                      v-model="menu4"
                      :close-on-content-click="false"
                      :nudge-left="40"
                      :return-value.sync="time"
                      translate="scale-transition"
                      offset-y
                      min-width="290px"
                      max-width="290px"
                    >
                    <template v-slot:activator="{on}">
                      <v-text-field
                        v-model="endTime"
                        label="End Time"
                        :rules="[values => !!value || 'Required' ]"
                        v-on="on"
                        required
                      >
                      </v-text-field>
                    </template>
                    <v-time-picker
                      v-if="menu4"
                      v-model="endTime"
                      full-width
                    ></v-time-picker>
                  </v-menu>
                </v-col>
              </v-row>
            </v-container>
            <v-btn
              :disable="!valid"
              x-large
              @click="Submit"
            >
            Create Event
            </v-btn>
          </v-col>
        </v-row>
      </v-form>
    </v-app>
  </div>
</template>

<script>
import axios from "axios";
export default {
  props: ["id"],
  components:{},
  data:() =>({
    currentDate: new Date().toISOString().substr(0,10),
    valid: true,
    topMenu,time:null,
    menu1,menu2,menu3,menu4:false,
    EventName:"",
    EventDescription:"",
    eventDescriptionRule: 
    [value => (values ||'').length < 700 ||'max 700 characters'],
    startDate,startTime,endDate,endTime:null,
    eventNameRule:  
    [value => !!value || 'Bracket name required',
    value => (value || '').length > 5 || 'Min 5 characters', 
    value => (value || '').length < 75 || 'Max 75 characters']
  }),
  methods: {
    Submit(){
      this.$refs.form.validate()
      axios.post("https://localhost:44352/api/events/createEvent/${this.Event}",
      {
        EventName: this.EventName,
        Address: this.eventAddress,
        Description: this.EventDescription,
        StartDate: this.startDate + this.startTime,
        EndDate: this.endDate + this.endTime
      }
      );
      setTimeout((this.$store.dispatch('createEvent', this.EventInfo),500))
      this.$refs.form.reset()
    }
  },
}
</script>