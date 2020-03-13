import {Url} from "./../constants/UrlConstants"

export default function qsArrayStringify(array,propName) {
   
  let queryString = ""

   for(let i =0 ; i < array.length; i++){
      if(i !== 0){
          queryString +="&"
      }
      queryString+= propName  + Url.equal + array[i];
   }

   return queryString;
  }