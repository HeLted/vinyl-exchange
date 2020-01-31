import React from "react"

export default function Label(props){
return <label htmlFor={props.forId}>{props.value}</label>
  
}