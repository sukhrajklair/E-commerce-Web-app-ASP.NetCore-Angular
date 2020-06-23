import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
import { Router } from "@angular/router";
@Component({
  selector: "the-cart",
  templateUrl: "cart.component.html",
  styleUrls: []
})

export class Cart {
  public order;
  constructor(private data: DataService, private router: Router) {
    this.order = data.order;
  }

  onCheckout() {
    if (this.data.loginRequired) {
      //forced login
      this.router.navigate(["login"]);
    } else {
      //go to chekcout
      this.router.navigate(["checkout"]);
    }
  }
}