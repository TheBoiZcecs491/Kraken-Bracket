<template>
    <v-row justify="space-around">
          <v-col class="text-center" cols="12" sm="5">
            <v-text-field
              v-model="name"
              label="Name"
              placeholder="名前"
              outlined
            ></v-text-field>

            <v-text-field
              v-model="age"
              label="Age"
              placeholder="年齢"
              outlined
            ></v-text-field>

            <v-text-field
              v-model="location"
              label="Location"
              placeholder="場所"
              outlined
            ></v-text-field>

            <v-text-field
              v-model="docID"
              label="Document ID"
              placeholder="ドキュメント"
              outlined
            ></v-text-field>

            <div>
              <v-btn x-large @click="fetch">取得</v-btn>
              <v-btn x-large @click="submit">追加</v-btn>
              <v-btn x-large @click="update">更新</v-btn>
              <v-btn x-large @click="remove">削除</v-btn>
            </div>
            
             
          </v-col>
</template>