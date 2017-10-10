﻿import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Server } from '../shared/server.type';
import { SeverityLevel } from '../../shared/severity-level.enum';
import { ActivatedRoute } from '@angular/router';
import { AppSettings } from '../../../shared/app-settings.type';
import { VulnerabilityService } from '../../shared/vulnerability.service';
import { UserRoleResult } from '../../shared/user-role-result.type';
import { UserRole } from '../../shared/user-role.type';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'server-list',
    templateUrl: './server-list.component.html'
})

export class ServerListComponent implements OnInit {
    serverVulnerabilities: Server[];
    severityLevel=SeverityLevel
    remediationResponses=AppSettings.REMEDIATION_RESPONSES;
    public maxSize: number = 5;
    public totalItems: number = 100;
    public currentPage: number = 1;
    public numPages: number = 100;
    public itemsPerPage: number=10;
    private sub: any;
    id: string;
    reportID: string;
    reportTypes: UserRole[];
    isBrowser: boolean;

    constructor(private router: Router,private vulnerabilityService: VulnerabilityService,private route: ActivatedRoute) { 
       this.isBrowser=true;
    }
        
    ngOnInit() { 

       this.getReportTypes()
        //this.serverVulnerabilities=[{ ip:"10.1.10.34", hostname:"corpsecterm", numberOfFindings: 7, highestSeverity: SeverityLevel.High, oldestFindingDate: "01/01/1970", status: "Open"}]

    }

    getReportTypes(){
        this.sub = this.route.params.subscribe(params => {
           this.vulnerabilityService.getUserReportRoles()
                .then (results =>{
                    this.reportTypes=results.results
                    this.reportID=String(this.reportTypes[0].reportNameID)
                    this.getItems(1)
                });

        });

    }
    ngOnDestroy(){
      this.sub.unsubscribe();
    }
    getItems(pageNo: number) {
        this.sub = this.route.params.subscribe(params => {
           this.id = params['id'];

        this.vulnerabilityService.getServerSummaries(this.reportID, pageNo, this.itemsPerPage)
           .then (results =>{
               this.serverVulnerabilities=results.results
               this.totalItems=results.count
           });
        });

    }
    loadReport(value: string){
     this.reportID=value;
     this.getItems(1);
    }
    navigateTo(value: string){
      if (value == 'Server Summary'){
          this.router.navigate(['/vulnerabilities/server']);
      }
      else if (value == "Vulnerability Details"){
          this.router.navigate(['/vulnerabilities/details']);
      }
    }
    public onSubmit(form: NgForm) { 
     console.log('I submitted'); 
     let values: Server[]= new Array<Server>();
     for(var i=0;i<this.itemsPerPage;i++){
        console.log(form.value['ipAddress-'+String(i)]);

        let server = new Server();
        server.ipAddress = form.value['ipAddress-'+String(i)];
        server.decision=form.value['decision-'+String(i)];
        server.comments=form.value['comments-'+String(i)];
        server.id=form.value['uniqueID-'+String(i)];

        if(server.decision){
           values.push(server);
        }


     }
     console.log(form.value);
     if(values.length>0){
        this.vulnerabilityService.uploadServerDecisions(this.reportID,values)
     }

  }
 
  public setPage(pageNo: number): void {
    this.currentPage = pageNo;
  }
 
  public pageChanged(event: any): void {
    console.log('Page changed to: ' + event.page);
    console.log('Number items per page: ' + event.itemsPerPage);
    this.getItems(event.page)
  }
}