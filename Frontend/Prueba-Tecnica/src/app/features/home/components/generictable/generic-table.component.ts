import { Component, Input, Output, EventEmitter, ViewChild, type OnInit } from "@angular/core"
import { CommonModule } from "@angular/common"
import { MatTableModule, MatTable } from "@angular/material/table"
import { MatSortModule, MatSort } from "@angular/material/sort"
import { MatPaginatorModule, MatPaginator, type PageEvent } from "@angular/material/paginator"
import { MatButtonModule } from "@angular/material/button"
import { MatIconModule } from "@angular/material/icon"
import { MatMenuModule } from "@angular/material/menu"

export interface TableColumn {
    name: string
    property: string
    sortable: boolean
    visible: boolean
}

@Component({
    selector: "app-generic-table",
    standalone: true,
    imports: [
        CommonModule,
        MatTableModule,
        MatSortModule,
        MatPaginatorModule,
        MatButtonModule,
        MatIconModule,
        MatMenuModule,
    ],
    template: `
    <div class="table-container">
      <table mat-table [dataSource]="data" matSort class="mat-elevation-z1">
        <!-- Dynamic columns -->
        <ng-container *ngFor="let column of columns" [matColumnDef]="column.property">
          <th mat-header-cell *matHeaderCellDef [mat-sort-header]="column.sortable ? column.property : ''">
            {{ column.name }}
            <button *ngIf="column.sortable" mat-icon-button class="sort-icon">
              <mat-icon>unfold_more</mat-icon>
            </button>
          </th>
          <td mat-cell *matCellDef="let element">{{ element[column.property] }}</td>
        </ng-container>

        <!-- Actions Column -->
        <ng-container matColumnDef="actions">
          <th mat-header-cell *matHeaderCellDef>{{ actionsColumnName }}</th>
          <td mat-cell *matCellDef="let element">
            <button mat-icon-button [matMenuTriggerFor]="menu" aria-label="Actions">
              <mat-icon>more_vert</mat-icon>
            </button>
            <mat-menu #menu="matMenu">
             
              <button mat-menu-item (click)="onEdit.emit(element)">
                <mat-icon>edit</mat-icon>
                <span>Edit</span>
              </button>
              <button mat-menu-item (click)="onDelete.emit(element)">
                <mat-icon>delete</mat-icon>
                <span>Delete</span>
              </button>
            </mat-menu>
          </td>
        </ng-container>
    
        <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
        <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        
        <!-- No data row -->
        <tr class="mat-row" *matNoDataRow>
          <td class="mat-cell no-data-cell" [attr.colspan]="displayedColumns.length">
            No hay datos
          </td>
        </tr>
      </table>

      <div class="pagination-info">
        {{ paginationLabel }}
      </div>

      <mat-paginator 
        [pageSizeOptions]="[5, 10, 25, 100]"
        [pageSize]="pageSize"
        [length]="totalItems"
        (page)="pageChanged($event)"
        showFirstLastButtons>
      </mat-paginator>
    </div>
  `,
    styles: [
        `
    .table-container {
      width: 100%;
      overflow: auto;
      background-color: white;
      border-radius: 4px;
      box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
    }
    
    table {
      width: 100%;
    }
    
    th.mat-header-cell {
      font-weight: 500;
      color: #495057;
      background-color: #f8f9fa;
      padding: 12px 16px;
    }
    
    td.mat-cell {
      padding: 12px 16px;
      color: #212529;
    }
    
    .sort-icon {
      width: 24px;
      height: 24px;
      line-height: 24px;
      margin-left: 4px;
    }
    
    .no-data-cell {
      text-align: center;
      padding: 24px;
      font-style: italic;
      color: #6c757d;
    }
    
    .pagination-info {
      padding: 12px 16px;
      color: #6c757d;
      font-size: 14px;
      display: flex;
      align-items: center;
      border-top: 1px solid #e9ecef;
    }
  `,
    ],
})
export class GenericTableComponent implements OnInit {
    @Input() columns: TableColumn[] = []
    @Input() data: any[] = []
    @Input() totalItems = 0
    @Input() pageSize = 10
    @Input() actionsColumnName = "Acciones"
    @Input() showActions: boolean = true;

    @Output() onView = new EventEmitter<any>()
    @Output() onEdit = new EventEmitter<any>()
    @Output() onDelete = new EventEmitter<any>()
    @Output() onPageChange = new EventEmitter<{ pageIndex: number; pageSize: number }>()

    @ViewChild(MatSort) sort!: MatSort
    @ViewChild(MatPaginator) paginator!: MatPaginator
    @ViewChild(MatTable) table!: MatTable<any>

    displayedColumns: string[] = []
    currentPage = 0

    ngOnInit() {
        this.displayedColumns = this.columns.filter((column) => column.visible).map((column) => column.property)


        if (this.showActions) {
            this.displayedColumns.push("actions");
        }
    }

    pageChanged(event: PageEvent) {
        this.currentPage = event.pageIndex
        this.onPageChange.emit({
            pageIndex: event.pageIndex,
            pageSize: event.pageSize,
        })
    }

    get paginationLabel(): string {
        const start = this.currentPage * this.pageSize + 1
        const end = Math.min((this.currentPage + 1) * this.pageSize, this.totalItems)
        return `Mostrando ${start} a ${end} de ${this.totalItems} entradas`
    }
}
