import React from "react"

function Label(props){
return <label className="property-text-nm" htmlFor={props.forId}>{props.value}</label>
  
}

export default Label;