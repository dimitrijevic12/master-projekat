import React, { useEffect, useState } from "react";
import { getAccommodationById } from "../../actions/actionsAccommodation";
import { connect } from "react-redux";
import { compose } from "redux";

function ReviewAccommodationInShoppingCart(props) {
  const [item, setItem] = useState({});
  const [showPostModal, setShowPostModal] = useState(false);

  useEffect(() => {
    debugger;
    props.getAccommodationById(
      localStorage.getItem("shoppingAccommodation-id")
    );
  }, []);

  const Accommodation = () => {
    debugger;
    if (props.loadedImage === undefined) {
      return null;
    }
    debugger;
    return (
      <img
        src={"data:image/jpg;base64," + props.loadedImage.fileContents}
        style={{ width: 600, height: 320 }}
        className="mb-3"
      />
    );
  };

  const displayModalPost = (item) => {
    if (item != undefined) {
      setItem(item);
    }
    debugger;
    if (
      localStorage.getItem("shoppingCart") === null ||
      localStorage.getItem("shoppingCart") === ""
    ) {
      var shoppingCartList = [];
    } else {
      var shoppingCartList = JSON.parse(localStorage.getItem("shoppingCart")); //get them back
    }
    shoppingCartList = shoppingCartList.filter((cartItem) => {
      if (cartItem.type === "accommodation") {
        return cartItem.item.id !== item.id;
      } else {
        return cartItem;
      }
    });
    localStorage.setItem("shoppingCart", JSON.stringify(shoppingCartList));
    window.location = "/items-in-shopping-cart";
  };

  if (props.accommodation === undefined) {
    return null;
  }

  return (
    <React.Fragment>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <Accommodation />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label className="label">Name:</label>
            <input
              type="text"
              name="name"
              class="form-control"
              id="name"
              value={props.accommodation.name}
              disabled={true}
              placeholder="Enter name of product"
            />
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Cost per night:</label>
            <input
              type="text"
              name="price"
              class="form-control"
              id="price"
              value={props.accommodation.costPerNight + " €"}
              disabled={true}
              placeholder="Enter price"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Description:</label>
            <textarea
              name="description"
              value={props.accommodation.description}
              cols="40"
              rows="5"
              class="form-control"
              disabled={true}
              placeholder="Enter promotion/action"
            ></textarea>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Address:</label>
            <input
              type="text"
              name="address"
              class="form-control"
              id="address"
              value={props.accommodation.address}
              disabled={true}
              placeholder="Enter address"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">City:</label>
            <input
              type="text"
              name="city"
              class="form-control"
              id="city"
              value={props.accommodation.city}
              disabled={true}
              placeholder="Enter city"
            />
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Start Date:</label>
            <div className="d-block w-100">
              <input
                type="text"
                name="quantity"
                class="form-control"
                id="quantity"
                value={localStorage
                  .getItem("shopping-accommodation-name")
                  .substring(
                    localStorage.getItem("shopping-accommodation-name").length -
                      21,
                    localStorage.getItem("shopping-accommodation-name").length -
                      11
                  )}
                disabled={true}
                placeholder="Enter quantity"
              />
            </div>
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">End Date:</label>
            <div className="d-block w-100">
              <input
                type="text"
                name="quantity"
                class="form-control"
                id="quantity"
                value={localStorage
                  .getItem("shopping-accommodation-name")
                  .substring(
                    localStorage.getItem("shopping-accommodation-name").length -
                      10,
                    localStorage.getItem("shopping-accommodation-name").length
                  )}
                disabled={true}
                placeholder="Enter quantity"
              />
            </div>
          </div>
        </div>
      </div>
      <div className="mt-5">
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="tag">Quantity:</label>
            <input
              type="number"
              name="quantity"
              class="form-control"
              id="quantity"
              value={localStorage.getItem("quantity-for-shopping-item")}
              disabled={true}
              placeholder="Enter quantity"
            />
          </div>
        </div>
        <div className="d-inline-flex w-50">
          <div class="form-group w-100 pr-5">
            <label for="location">Total price:</label>
            <input
              type="text"
              name="quantity"
              class="form-control"
              id="quantity"
              value={
                parseInt(localStorage.getItem("quantity-for-shopping-item")) *
                  parseInt(props.accommodation.costPerNight) *
                  parseInt(
                    getDaysBetween(
                      localStorage.getItem("shopping-accommodation-name")
                    )
                  ) +
                " €"
              }
              disabled={true}
              placeholder="Enter quantity"
            />
          </div>
        </div>
      </div>
      <div style={{ textAlign: "center" }} className="mt-5 pb-5">
        <button
          onClick={() => displayModalPost(props.accommodation)}
          className="btn btn-primary"
        >
          Remove item from cart
        </button>
      </div>
    </React.Fragment>
  );
}

const getDaysBetween = (name) => {
  debugger;
  const endDate = name.substring(name.length - 10, name.length);
  const startDate = name.substring(name.length - 21, name.length - 11);
  const date1 = parseDate(startDate);
  const date2 = parseDate(endDate);
  return dateDiff(date1, date2);
};

const parseDate = (str) => {
  var mdy = str.split("/");
  return new Date(+mdy[2], mdy[1] - 1, +mdy[0]);
};

const dateDiff = (first, second) => {
  return Math.round((second - first) / (1000 * 60 * 60 * 24));
};

const mapStateToProps = (state) => ({
  accommodation: state.accommodation,
  loadedImage: state.loadedImage,
});

export default compose(
  connect(mapStateToProps, {
    getAccommodationById,
  })
)(ReviewAccommodationInShoppingCart);
