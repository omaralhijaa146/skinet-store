import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss']
})
export class ServerErrorComponent implements OnInit{

  error:any;

  constructor(private router:Router) {

    const navigation=this.router.getCurrentNavigation();
    this.error=navigation?.extras?.state?.['state']?.['error'];
  }

  ngOnInit(): void {

    }

}
