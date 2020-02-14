import React from "react";

 function SingleSelect(props) {
  const options = props.options.map(optionsObj => {
    return <option key={optionsObj.id} value={optionsObj.id}>{optionsObj.name}</option>;
    //mapping requires to have name and id in props passed object
  });

  const defaultOptionDisplay = props.value === "" ? "block" : "none";

  const defaultOption = (
    <option
      key="default"
      style={{ display: defaultOptionDisplay }}
    >
      {props.defaultOptionLabel}
    </option>
  );

  return (
    <select
      className="form-control"
      id={props.id}
      name={props.id}
      value={props.value}
      onChange={props.onChange}
    >
      {defaultOption}
      {options}
    </select>
  );
}

export default SingleSelect;