import React from 'react';
import { withRouter } from "react-router-dom";
import SearchShopsContainer from "./searchShops/SearchShopsContainer"
import ShopsTableContainerWrapper from "./ShopsTableContainer";

function ShopsComponent(props){

  const Button = withRouter(({ history }) => (
    <button
      type="button"
      className="btn btn-primary w-100"
      onClick={() => {
        history.push("/shops/addshop");
      }}
    >
      Add Shop
    </button>
  ));


  return( <div className="container-fluid">
  <div className="row">
    <div className="custom-container col-lg-3 col-sm-12 col-xs-12 center-block">
      <div className="container-fluid justify-content-center">
        <SearchShopsContainer
          onUpdateSearchValue={props.functions.onUpdateSearchValue}
          searchValue={props.data.searchValue}
        />

        {/* <FilterShopsContainer
          functions={{
            onUpdateFilterValue: props.functions.onUpdateFilterValue
          }}
        /> */}

        <br />
        <Button />
      </div>
    </div>
    <div className="custom-container col-lg-9 col-sm-12 col-xs-12 align-items-center justify-content-center">
       <ShopsTableContainerWrapper
        data={{
          searchValue: props.data.searchValue,
          filterStyleIds: props.data.filterStyleIds
        }}
   />
    </div>
  </div>
</div>)
}

export default ShopsComponent;