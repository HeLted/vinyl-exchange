import React from "react";
import GoToSaleButton from "./../../../common/GoToSaleButton"
import BorderSpinner from "./../../../common/spinners/BorderSpinner"

function ReleaseSalesComponent(props) {
  const salesRows = props.data.sales.map(saleObj => {
    return (
      <tr key={saleObj.id}>
        <td>Seller -> {saleObj.sellerUsername}</td>
        <td>Vinyl Grade -> {saleObj.vinylCondition}</td>
        <td>Sleeve Grade -> {saleObj.sleeveCondition}</td>
        <td><GoToSaleButton data={{saleId:saleObj.id}}/></td>
      </tr>
    );
  });

  const component = props.data.isLoading ? (<tr><td><BorderSpinner/></td></tr>) : salesRows

  return (
    <table className="release-sales-table table table-hover">
      <tbody>{component}</tbody>
    </table>
  );
}

export default ReleaseSalesComponent;
