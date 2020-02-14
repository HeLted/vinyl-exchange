import React from "react"

function Label(props){
return <label htmlFor={props.forId}>{props.value}</label>
  
}

export default Label;