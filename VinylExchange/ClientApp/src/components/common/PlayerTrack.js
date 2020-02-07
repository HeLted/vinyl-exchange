import React from "react";

export default function PlyerTrack(props) {
  console.log(props)
  return (
    <li>
      <a href={props.path}>
        {props.name}
      </a>
    </li>
  );
}
