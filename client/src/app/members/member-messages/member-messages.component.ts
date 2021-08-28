import { ChangeDetectionStrategy, Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MemberMessagesComponent implements OnInit {
   @Input() messages: Message[];
   @Input() username: string;
   messageContent: string;

   @ViewChild('messageForm') messageForm: NgForm;

  constructor(public messageService: MessageService) { }

  ngOnInit(): void { }

  sendMessage() {
    this.messageService.sendMessage(this.username, this.messageContent).then(() => {
      this.messageForm.reset();
    })
  }
}
