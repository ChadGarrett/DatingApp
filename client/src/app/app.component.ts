import { Component, OnInit } from '@angular/core';
import { User } from './models/User';
import { AccountService } from './_services/account.service';
import { PresenceService } from './_services/presence.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;

  constructor(private accountService: AccountService, private presense: PresenceService) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    // var userJson = localStorage.getItem('user');

    // if (userJson) {
    //   const user: User = JSON.parse(userJson);
    //   this.accountService.setCurrentUser(user);
    // }



    const user: User = JSON.parse(localStorage.getItem('user'));
    if (user) {
      this.accountService.setCurrentUser(user);
      this.presense.createHubConnection(user);
    }
  }
}
