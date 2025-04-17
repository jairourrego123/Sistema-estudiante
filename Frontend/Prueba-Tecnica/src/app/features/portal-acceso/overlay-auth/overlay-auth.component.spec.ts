import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverlayAuthComponent } from './overlay-auth.component';

describe('OverlayAuthComponent', () => {
  let component: OverlayAuthComponent;
  let fixture: ComponentFixture<OverlayAuthComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OverlayAuthComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OverlayAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
